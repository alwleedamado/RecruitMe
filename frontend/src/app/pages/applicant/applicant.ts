import { Component, OnInit, signal, ViewChild } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table, TableModule } from 'primeng/table';
import { CommonModule } from '@angular/common';
import { AbstractControl, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { RatingModule } from 'primeng/rating';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';
import { SelectModule } from 'primeng/select';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputNumberModule } from 'primeng/inputnumber';
import { DialogModule } from 'primeng/dialog';
import { TagModule } from 'primeng/tag';
import { InputIconModule } from 'primeng/inputicon';
import { IconFieldModule } from 'primeng/iconfield';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { filter, from, map } from 'rxjs';
import { concatMap, toArray } from 'rxjs/operators';
import { ApplicantService } from '../service/applicant.service';
import { ApplicantDto } from '@/app/models/applicant';

interface Column {
    field: string;
    header: string;
    customExportHeader?: string;
}

interface ExportColumn {
    title: string;
    dataKey: string;
}

interface FormData {
    id?: number;
    fullName: string;
    email: string;
    password: string;
    confirmPassword: string;
    hireDate: Date;
    salary: number;
}

@Component({
    selector: 'app-hr',
    standalone: true,
    imports: [
        CommonModule,
        TableModule,
        FormsModule,
        ButtonModule,
        RippleModule,
        ToastModule,
        ToolbarModule,
        RatingModule,
        InputTextModule,
        TextareaModule,
        SelectModule,
        RadioButtonModule,
        InputNumberModule,
        DialogModule,
        TagModule,
        InputIconModule,
        IconFieldModule,
        ConfirmDialogModule,
        ReactiveFormsModule
    ],
    template: `
        <p-toolbar styleClass="mb-6">
            <ng-template #start>
                <p-button label="New" icon="pi pi-plus" severity="secondary" class="mr-2" (onClick)="openNew()" />
                <p-button severity="secondary" label="Delete" icon="pi pi-trash" outlined (onClick)="deleteSelected()" [disabled]="!selectedApplicants || !selectedApplicants.length" />
            </ng-template>

            <ng-template #end>
                <p-button label="Export" icon="pi pi-upload" severity="secondary" (onClick)="exportCSV()" />
            </ng-template>
        </p-toolbar>

        <p-table
            #dt
            [value]="applicants()"
            [rows]="10"
            [columns]="cols"
            [paginator]="true"
            [globalFilterFields]="['name', 'country.name', 'representative.name', 'status']"
            [tableStyle]="{ 'min-width': '75rem' }"
            [(selection)]="selectedApplicants"
            [rowHover]="true"
            dataKey="id"
            currentPageReportTemplate="Showing {first} to {last} of {totalRecords} hrEmployee"
            [showCurrentPageReport]="true"
            [rowsPerPageOptions]="[10, 20, 30]"
        >
            <ng-template #caption>
                <div class="flex items-center justify-between">
                    <h5 class="m-0">Manage Products</h5>
                    <p-iconfield>
                        <p-inputicon styleClass="pi pi-search" />
                        <input pInputText type="text" (input)="onGlobalFilter(dt, $event)" placeholder="Search..." />
                    </p-iconfield>
                </div>
            </ng-template>
            <ng-template #header>
                <tr>
                    <th style="width: 3rem">
                        <p-tableHeaderCheckbox />
                    </th>
                    <th style="min-width: 16rem">ID</th>
                    <th pSortableColumn="name" style="min-width:16rem">
                        Name
                        <p-sortIcon field="fullName" />
                    </th>
                    <th>
                        Email
                        <p-sortIcon field="email" />
                    </th>
                    <th style="min-width: 12rem"></th>
                </tr>
            </ng-template>
            <ng-template #body let-hrEmployee>
                <tr>
                    <td style="width: 3rem">
                        <p-tableCheckbox [value]="hrEmployee" />
                    </td>
                    <td style="min-width: 16rem">{{ hrEmployee.id }}</td>
                    <td style="min-width: 16rem">{{ hrEmployee.fullName }}</td>
                    <td>{{ hrEmployee.email }}</td>
                    <td>
                        <p-button icon="pi pi-pencil" class="mr-2" [rounded]="true" [outlined]="true" (click)="editHrEmployee(hrEmployee)" />
                        <p-button icon="pi pi-trash" severity="danger" [rounded]="true" [outlined]="true" (click)="deleteEmployee(hrEmployee)" />
                    </td>
                </tr>
            </ng-template>
        </p-table>

        <p-dialog [(visible)]="hrEmployeeDialog" [style]="{ width: '450px' }" header="Hr Details" [modal]="true">
            <ng-template #content>
                <div class="flex flex-col gap-6" [formGroup]="employeeForm">
                    <div>
                        <label for="name" class="block font-bold mb-3">Name</label>
                        <input type="text" pInputText id="name" formControlName="fullName" required autofocus fluid />
                        @if (employeeForm.controls['fullName'].touched && employeeForm.controls['fullName'].hasError('required')) {
                            <small class="text-red-500">Full name is required.</small>
                        }
                    </div>

                    <div>
                        <label for="email" class="block font-bold mb-3">Email</label>
                        <input type="text" pInputText id="email" formControlName="email" required autofocus fluid />
                        @if (employeeForm.controls['email'].touched && employeeForm.controls['email'].hasError('required')) {
                            <small class="text-red-500">Email is required.</small>
                        }
                        @if (employeeForm.controls['email'].touched && employeeForm.controls['email'].hasError('email')) {
                            <small class="text-red-500">Please enter a valid email.</small>
                        }
                    </div>
                    <div class="flex flex-col gap-6">
                        <div>
                            <label for="password" class="block font-bold mb-3">Password</label>
                            <input type="text" pInputText id="password" formControlName="password" required autofocus fluid />
                            @if (employeeForm.controls['password'].touched && employeeForm.controls['password'].hasError('required')) {
                                <small class="text-red-500">Password is required.</small>
                            }
                        </div>
                        <div>
                            <label for="confirmPassword" class="block font-bold mb-3">Confirm Password</label>
                            <input type="text" pInputText id="confirmPassword" formControlName="confirmPassword" required autofocus fluid />
                            @if (employeeForm.controls['confirmPassword'].touched && employeeForm.controls['confirmPassword'].hasError('required')) {
                                <small class="text-red-500">Confirm Password is required.</small>
                            }
                            @if (employeeForm.controls['confirmPassword'].touched && employeeForm.controls['confirmPassword'].hasError('mismatch')) {
                                <small class="text-red-500">Passwords do not match.</small>
                            }
                        </div>
                    </div>

                    <div>
                        <label for="salary" class="block font-bold mb-3">Salary</label>
                        <input type="text" pInputText id="salary" formControlName="salary" required autofocus fluid />
                        @if (employeeForm.controls['salary'].touched && employeeForm.controls['salary'].hasError('required')) {
                            <small class="text-red-500">Salary is required.</small>
                        }
                        @if (employeeForm.controls['salary'].touched && employeeForm.controls['salary'].hasError('min')) {
                            <small class="text-red-500">Salary must be a positive value.</small>
                        }
                    </div>
                </div>
            </ng-template>

            <ng-template #footer>
                <p-button label="Cancel" icon="pi pi-times" text (click)="hideDialog()" />
                <p-button label="Save" icon="pi pi-check" (click)="save()" />
            </ng-template>
        </p-dialog>

        <p-confirmdialog [style]="{ width: '450px' }" />
    `,
    providers: [MessageService, ApplicantService, ConfirmationService]
})
export class Applicants implements OnInit {
    hrEmployeeDialog: boolean = false;

    applicants = signal<ApplicantDto[]>([]);

    employeeForm: FormGroup;

    selectedApplicants!: ApplicantDto[] | null;

    currentEmployee = signal<ApplicantDto>({} as ApplicantDto);

    @ViewChild('dt') dt!: Table;

    exportColumns!: ExportColumn[];

    cols!: Column[];

    constructor(
        private applicantService: ApplicantService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService,
        private fb: FormBuilder
    ) {
        this.employeeForm = this.fb.group({
            fullName: ['', [Validators.required]],
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required]],
            confirmPassword: ['', [Validators.required]],
            hireDate: [new Date(), [Validators.required]],
            salary: [0, [Validators.required, Validators.min(0)]]
        });
        this.employeeForm.controls['confirmPassword'].addValidators(createPasswordVAlidator(this.employeeForm.controls['password']));
    }

    exportCSV() {
        this.dt.exportCSV();
    }

    ngOnInit() {
        this.applicants = this.applicantService.list;
        this.loadDemoData();
    }

    loadDemoData() {
        this.applicantService.loadList();
        
        this.cols = [
            { field: 'id', header: 'Id', customExportHeader: 'ID' },
            { field: 'fullName', header: 'Name' },
            { field: 'email', header: 'Email' },
            { field: 'hireDate', header: 'Hire Date' }
        ];

        this.exportColumns = this.cols.map((col) => ({ title: col.header, dataKey: col.field }));
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    openNew() {
        this.employeeForm.reset();
        this.hrEmployeeDialog = true;
    }

    editHrEmployee(hrEmployee: ApplicantDto) {
        this.employeeForm.reset();
        this.employeeForm.patchValue(hrEmployee);
        this.hrEmployeeDialog = true;
    }

    deleteSelected() {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete the selected applicants?',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            accept: async () => {
                from(this.selectedApplicants || [])
                    .pipe(
                        filter((x) => x !== null),
                        map((employee) => employee!.id),
                        concatMap((id) => this.applicantService.delete(id)),
                        toArray()
                    )
                    .subscribe((employee) => {
                        this.messageService.add({
                            severity: 'success',
                            summary: 'Successful',
                            detail: 'Employee Deleted',
                            life: 3000
                        });
                    });
            }
        });
    }

    hideDialog() {
        this.hrEmployeeDialog = false;
    }

    deleteEmployee(hrEmployee: ApplicantDto) {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete ' + hrEmployee.fullName + '?',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
                this.applicantService.delete(hrEmployee.id);
                this.messageService.add({
                    severity: 'success',
                    summary: 'Successful',
                    detail: 'Hr Deleted',
                    life: 3000
                });
            }
        });
    }

    save() {
        if (!this.employeeForm.invalid) {
            this.applicantService.create(this.employeeForm.value);
        }
    }
}

function createPasswordVAlidator(passwordCtl: AbstractControl): ValidatorFn {
    return (confirmPasswordCtl: AbstractControl): ValidationErrors | null => {
        const password = passwordCtl.value;
        const confirmPassword = confirmPasswordCtl.value;
        return password === confirmPassword ? null : { mismatch: true };
    };
}
