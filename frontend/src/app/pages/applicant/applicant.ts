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
import { ApplicantDto } from '@/app/models/applicant.model';

interface Column {
    field: string;
    header: string;
    customExportHeader?: string;
}

interface ExportColumn {
    title: string;
    dataKey: string;
}

@Component({
    selector: 'app-applicants',
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
            [globalFilterFields]="['fullName', 'email']"
            [tableStyle]="{ 'min-width': '75rem' }"
            [(selection)]="selectedApplicants"
            [rowHover]="true"
            dataKey="id"
            currentPageReportTemplate="Showing {first} to {last} of {totalRecords} Applicants"
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
            <ng-template #body let-applicant>
                <tr>
                    <td style="width: 3rem">
                        <p-tableCheckbox [value]="applicant" />
                    </td>
                    <td style="min-width: 16rem">{{ applicant.id }}</td>
                    <td style="min-width: 16rem">{{ applicant.fullName }}</td>
                    <td>{{ applicant.email }}</td>
                    <td>
                        <p-button icon="pi pi-eye" class="mr-2" [rounded]="true" [outlined]="true" (click)="viewApplicant(applicant.id)" />
                    </td>
                </tr>
            </ng-template>
        </p-table>

        <p-confirmdialog [style]="{ width: '450px' }" />
    `,
    providers: [MessageService, ApplicantService, ConfirmationService]
})
export class Applicants implements OnInit {
    applicants = signal<ApplicantDto[]>([]);

    selectedApplicants!: ApplicantDto[] | null;

    currentApplicant = signal<ApplicantDto>({} as ApplicantDto);

    @ViewChild('dt') dt!: Table;

    exportColumns!: ExportColumn[];

    cols!: Column[];

    constructor(
        private applicantService: ApplicantService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService,
    ) {
     
    }

    exportCSV() {
        this.dt.exportCSV();
    }

    ngOnInit() {
        this.applicants = this.applicantService.list;
    }


    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
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

   viewApplicant(id: number) {

   }
}