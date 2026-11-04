import { Injectable, signal, Signal, WritableSignal } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { DataService } from './DataService';
import { ApplicationDto } from '@/app/models/application.model';

@Injectable()
export class ApplicationsService extends DataService<ApplicationDto> {
    protected endpoint: string = 'applications';
    constructor(httpClient: HttpClient) {
        super(httpClient);
    }
}
