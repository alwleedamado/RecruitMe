import { environment } from '@/environments/environment';
import { HttpClient } from '@angular/common/http';
import { signal } from '@angular/core';
import { Observable } from 'rxjs';

export abstract class DataService<T extends {id: number}> {
    protected apiUrl: string = environment.apiUrl;

    protected abstract endpoint: string;

    list = signal<T[]>([]);
    loading = signal(false);

    constructor(protected httpClient: HttpClient) {}

    loadList() {
        this.httpClient.get<T[]>(`${this.apiUrl}/${this.endpoint}`).subscribe({
            next: (items) => {
                this.list.set(items);
                this.loading.set(false);
            },
            error: (error) => {
                console.error('Error fetching data:', error);
                this.loading.set(false);
            },
            complete: () => {
                console.log('Data fetching completed.');
            }
        });
    }

    getById(id: number): Observable<T> {
        return this.httpClient.get<T>(`${this.apiUrl}/${this.endpoint}/${id}`);
    }

    create(item: T): Observable<T> {
        return this.httpClient.post<T>(`${this.apiUrl}/${this.endpoint}`, item);
    }

    update(id: number, item: T): Observable<T> {
        return this.httpClient.put<T>(`${this.apiUrl}/${this.endpoint}/${id}`, item);
    }

    delete(id: number): Observable<void> {
        return this.httpClient.delete<void>(`${this.apiUrl}/${this.endpoint}/${id}`);
    }
}
