import { environment } from '@/environments/environment';
import { HttpClient } from '@angular/common/http';
import { computed, Injectable, signal } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, map, Observable, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private token = signal(localStorage.getItem('token'));

    public getToken(): string | null{
        return this.token();
    }
    isAuthenticated = computed(() => !!this.token());

    constructor(
        private router: Router,
        private http: HttpClient
    ) {}

    login(email: string, password: string, remeberMe: boolean) {
        return this.http.post<{ accessToken: string }>(`${environment.apiUrl}/auth/login`, { email, password }).pipe(
            tap((response) => {
                remeberMe && localStorage.setItem('token', response.accessToken);
                this.token.set(response.accessToken);
            }),
            catchError((error: any) => {
                this.token.set(null);
                localStorage.clear();
                console.error('Login failed:', error);
                return this.router.navigate(['/auth/login']);
            })
        );
    }

    logout() {
        localStorage.removeItem('token');
        this.token.set(null);
        this.router.navigate(['/login']);
    }
}
