import { Routes } from '@angular/router';
import { Empty } from './empty/empty';
import { Hr } from './hr/hr';
import { Applicants } from './applicant/applicant';
import { JobPostings } from './jobpostings/jobpostings';

export default [
    { path: 'hr', component: Hr },
    { path: 'applicants', component: Applicants },
    { path: 'job-postings', component: JobPostings },
    { path: 'empty', component: Empty },
    { path: '**', redirectTo: '/notfound' }
] as Routes;
