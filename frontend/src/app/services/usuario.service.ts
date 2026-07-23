import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StatusUsuario, Usuario } from '../core/models';

const API_URL = 'http://localhost:5000/api/usuarios';

@Injectable({ providedIn: 'root' })
export class UsuarioService {
  constructor(private http: HttpClient) {}

  listar(status?: StatusUsuario): Observable<Usuario[]> {
    const query = status === undefined || status === null ? '' : `?status=${status}`;
    return this.http.get<Usuario[]>(`${API_URL}${query}`);
  }

  criar(login: string, senha: string, status: StatusUsuario): Observable<Usuario> {
    return this.http.post<Usuario>(API_URL, { login, senha, status });
  }

  atualizar(id: number, senha: string | null, status: StatusUsuario | null): Observable<Usuario> {
    return this.http.put<Usuario>(`${API_URL}/${id}`, { senha, status });
  }
}
