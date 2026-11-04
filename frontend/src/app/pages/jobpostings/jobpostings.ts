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
import { HrService } from '../service/hr.service';
import { filter, from, map } from 'rxjs';
import { concatMap, toArray } from 'rxjs/operators';
import { DatePickerModule } from 'primeng/datepicker';
import { JobPostingDto, JobType } from '@/app/models/jobPosting.model';
import { JobPostingService } from '../service/jobPosting.service';

interface Column {
    field: string;
    header: string;
}

interface FormData {
    title: string;
    description: string;
    requriements: string;
    jobType: string;
    location?: string;
    endOn: Date;
}

@Component({
    selector: 'app-job-posting',
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
        ReactiveFormsModule,
        DatePickerModule
    ],
    template: `
        <p-toolbar styleClass="mb-6">
            <ng-template #start>
                <p-button label="New" icon="pi pi-plus" severity="secondary" class="mr-2" (onClick)="openNew()" />
                <p-button severity="secondary" label="Delete" icon="pi pi-trash" outlined (onClick)="deleteSelected()" [disabled]="!selectedJobPostings || !selectedJobPostings.length" />
            </ng-template>
        </p-toolbar>

        <p-table
            #dt
            [value]="jobPostings()"
            [rows]="10"
            [paginator]="true"
            [globalFilterFields]="['name', 'jobType', 'location', 'endOn']"
            [tableStyle]="{ 'min-width': '75rem' }"
            [(selection)]="selectedJobPostings"
            [rowHover]="true"
            dataKey="id"
            currentPageReportTemplate="Showing {first} to {last} of {totalRecords} job postings"
            [showCurrentPageReport]="true"
            [rowsPerPageOptions]="[10, 20, 30]"
        >
            <ng-template #caption>
                <div class="flex items-center justify-between">
                    <h5 class="m-0">Manage Job Postings</h5>
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
                    <th pSortableColumn="title" style="min-width:16rem">
                        Title
                        <p-sortIcon field="fullName" />
                    </th>
                    <th>
                        Job Type
                        <p-sortIcon field="jobType" />
                    </th>
                    <th>
                        Location
                        <p-sortIcon field="location" />
                    </th>
                    <th>
                        End Date
                        <p-sortIcon field="endOn" />
                    </th>
                    <th style="min-width: 12rem"></th>
                </tr>
            </ng-template>
            <ng-template #body let-jobPosting>
                <tr>
                    <td style="width: 3rem">
                        <p-tableCheckbox [value]="jobPosting" />
                    </td>
                    <td style="min-width: 16rem">{{ jobPosting.id }}</td>
                    <td style="min-width: 16rem">{{ jobPosting.title }}</td>
                    <td>{{ jobPosting.jobType }}</td>
                    <td>{{ jobPosting.location }}</td>
                    <td>{{ jobPosting.endOn }}</td>

                    <td>
                        <p-button icon="pi pi-pencil" class="mr-2" [rounded]="true" [outlined]="true" (click)="editJobPosting(jobPosting)" />
                        <p-button icon="pi pi-trash" severity="danger" [rounded]="true" [outlined]="true" (click)="deleteJobPosting(jobPosting)" />
                    </td>
                </tr>
            </ng-template>
        </p-table>

        <p-dialog [(visible)]="jobPostingsDialog" [style]="{ width: '450px' }" header="Hr Details" [modal]="true">
            <ng-template #content>
                <div class="flex flex-col gap-6" [formGroup]="jobPostingForm">
                    <div>
                        <label for="title" class="block font-bold mb-3">Title</label>
                        <input type="text" pInputText id="title" formControlName="title" required autofocus fluid />
                        @if (jobPostingForm.controls['title'].touched && jobPostingForm.controls['title'].hasError('required')) {
                            <small class="text-red-500">Title is required.</small>
                        }
                    </div>

                    <div>
                        <label for="jobType" class="block font-bold mb-3">Job Type</label>
                        <input type="text" pInputText id="jobType" formControlName="jobType" required autofocus fluid />
                        @if (jobPostingForm.controls['jobType'].touched && jobPostingForm.controls['jobType'].hasError('required')) {
                            <small class="text-red-500">Job type is required.</small>
                        }
                    </div>

                    <div>
                        <label for="location" class="block font-bold mb-3">Email</label>
                        <input type="text" pInputText id="location" formControlName="location" required autofocus fluid />
                        @if (jobPostingForm.controls['location'].touched && jobPostingForm.controls['location'].hasError('required')) {
                            <small class="text-red-500">Location is required for on-site and hybird job types.</small>
                        }
                    </div>

                    <div>
                        <label for="endOn" class="block font-bold mb-3">Hire Date</label>
                        <p-datePicker pInputText id="endOn" formControlName="endOn" required autofocus fluid />
                        @if (jobPostingForm.controls['endOn'].touched && jobPostingForm.controls['endOn'].hasError('required')) {
                            <small class="text-red-500">job posting end date is required.</small>
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
    providers: [MessageService, JobPostingService, ConfirmationService]
})
export class JobPostings implements OnInit {
    jobPostingsDialog: boolean = false;

    jobPostings = signal<JobPostingDto[]>([]);

    jobPostingForm: FormGroup;

    selectedJobPostings!: JobPostingDto[] | null;

    currentPosting = signal<JobPostingDto>({} as JobPostingDto);

    @ViewChild('dt') dt!: Table;

    constructor(
        private jobPostingService: JobPostingService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService,
        private fb: FormBuilder
    ) {
        this.jobPostingForm = this.fb.group({
            title: ['', [Validators.required]],
            jobType: ['', [Validators.required]],
            location: ['', [Validators.required]],
            requirements: ['', [Validators.required]],
            endOn: [new Date(), [Validators.required]]
        });
    }

    ngOnInit() {
        this.jobPostings = this.jobPostingService.list;
        this.loadData();
    }

    loadData() {
        this.jobPostingService.loadList();
    }

    onGlobalFilter(table: Table, event: Event) {
        table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
    }

    openNew() {
        this.jobPostingForm.reset();
        this.jobPostingsDialog = true;
    }

    editJobPosting(jobPosting: JobPostingDto) {
        this.jobPostingForm.reset();
        this.jobPostingForm.patchValue(jobPosting);
        this.jobPostingsDialog = true;
    }

    deleteSelected() {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete the selected postings?',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            accept: async () => {
                from(this.selectedJobPostings || [])
                    .pipe(
                        filter((x) => x !== null),
                        map((employee) => employee!.id),
                        concatMap((id) => this.jobPostingService.delete(id)),
                        toArray()
                    )
                    .subscribe((employee) => {
                        this.messageService.add({
                            severity: 'success',
                            summary: 'Successful',
                            detail: 'Job posting Deleted',
                            life: 3000
                        });
                    });
            }
        });
    }

    hideDialog() {
        this.jobPostingsDialog = false;
    }

    deleteJobPosting(jobPosting: JobPostingDto) {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete ' + jobPosting.title + '?',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
                this.jobPostingService.delete(jobPosting.id);
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
        if (!this.jobPostingForm.invalid) {
            this.jobPostingService.create(this.jobPostingForm.value).subscribe(() => {
                this.hideDialog();
            });
        }
    }
}

function validJobLocation(jobTpeCnt: AbstractControl): ValidatorFn {
    return (cnt: AbstractControl): ValidationErrors | null => {
        const jobType = jobTpeCnt.value;
        const value = cnt.value as string;
        if (jobType == JobType.OnSite || JobType.Hybird) {
            return value.trim().length == 0 ? { required: true } : null;
        }
        return null;
    };
}
