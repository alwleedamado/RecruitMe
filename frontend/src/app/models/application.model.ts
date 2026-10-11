import { JobPostingDto } from "./jobPosting.model";

export interface ApplicationDto {
    id: number;
    applicantId: number;
    jobId: number;
    jobPosting: JobPostingDto;
    status: ApplicationStatus;
}

export interface CreateApplication {
    jobId: number;
}

export enum ApplicationStatus {
    Draft,
    Submited,
    Accepted,
    Rejected
}