import { Injectable, signal, Signal, WritableSignal } from '@angular/core';
import { HrModel } from '@/app/models/hr.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { DataService } from './DataService';

@Injectable()
export class HrService extends DataService<HrModel> {
    protected endpoint: string = 'hr';
    constructor(httpClient: HttpClient) {
        super(httpClient);
    }
    
}
