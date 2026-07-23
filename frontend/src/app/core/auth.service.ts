import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { LoginResposta } from './models';

const CHAVE_TOKEN = 'gc_token';
const CHAVE_LOGIN = 'gc_login';
const API_URL = 'http://localhost:5000/api';

@Injectable({ providedIn: 'root' })
export class AuthService {
  usuarioLogado = signal<string | null>(localStorage.getItem(CHAVE_LOGIN));

  constructor(private http: HttpClient) {}

  login(login: string, senha: string): Observable<LoginResposta> {
    return this.http.post<LoginResposta>(`${API_URL}/auth/login`, { login, senha }).pipe(
      tap((resposta) => {
        localStorage.setItem(CHAVE_TOKEN, resposta.token);
        localStorage.setItem(CHAVE_LOGIN, resposta.login);
        this.usuarioLogado.set(resposta.login);
      })
    );
  }

  logout(): void {
    localStorage.removeItem(CHAVE_TOKEN);
    localStorage.removeItem(CHAVE_LOGIN);
    this.usuarioLogado.set(null);
  }

  obterToken(): string | null {
    return localStorage.getItem(CHAVE_TOKEN);
  }

  estaAutenticado(): boolean {
    return !!this.obterToken();
  }
}
