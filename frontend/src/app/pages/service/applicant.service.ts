import { Injectable, signal, Signal, WritableSignal } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { DataService } from './DataService';
import { ApplicantDto } from '@/app/models/applicant.model';

@Injectable()
export class ApplicantService extends DataService<ApplicantDto> {
    protected endpoint: string = 'hr';
    constructor(httpClient: HttpClient) {
        super(httpClient);
    }
}
