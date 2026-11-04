import { Injectable, signal, Signal, WritableSignal } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { DataService } from './DataService';
import { JobPostingDto } from '@/app/models/jobPosting.model';

@Injectable()
export class JobPostingService extends DataService<JobPostingDto> {
    protected endpoint: string = 'jobPostings';
    constructor(httpClient: HttpClient) {
        super(httpClient);
    }
}
