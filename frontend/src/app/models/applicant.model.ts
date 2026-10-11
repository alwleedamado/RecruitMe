export interface CreateApplicant {
    fullName: string;
    email: string;
    password: string;
}

export interface ApplicantDto {
    id: number;
    fullName: string;
    email: string;
    workExperiences: WorkExperienceDto[];
    skills: string[];
}

export interface WorkExperienceDto {
    companyName: string;
    description: string;
    startDate?: string;
    endDate?: string;
    applicaId: number;
}