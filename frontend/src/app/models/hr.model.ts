export interface RegisterHrRequest extends HrModel {
    password: string;
}

export interface HrModel {
    id: number;
    fullName: string;
    email: string;
    hireDate: Date;
    salary: number;
}

export interface UpdateHr {
    id: number;
    fullName: string;
    email: string;
}
