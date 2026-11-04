export interface JobPostingDto {
    id: number;
    title: string;
    description: string;
    requriements: string;
    jobType: string;
    location?: string;
    endOn: Date;
}

export interface CreateJobPosting {
    title: string;
    description: string;
    requriements: string;
    jobType: string;
    location?: string;
    endOn: Date;
}

export interface UpdateJobPosting extends CreateJobPosting {
    id: string;
    title: string;
    description: string;
    requriements: string;
    jobType: string;
    location?: string;
    endOn: Date;
}

export enum JobType {
    OnSite,
    Hybird,
    Remote
}