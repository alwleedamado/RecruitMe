export interface JobPostingDto {
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
