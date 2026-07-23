import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Colaborador } from '../core/models';

const API_URL = 'http://localhost:5000/api/colaboradores';

@Injectable({ providedIn: 'root' })
export class ColaboradorService {
  constructor(private http: HttpClient) {}

  listar(): Observable<Colaborador[]> {
    return this.http.get<Colaborador[]>(API_URL);
  }

  criar(nome: string, unidadeId: number, usuarioId: number): Observable<Colaborador> {
    return this.http.post<Colaborador>(API_URL, { nome, unidadeId, usuarioId });
  }

  atualizar(id: number, nome: string, unidadeId: number): Observable<Colaborador> {
    return this.http.put<Colaborador>(`${API_URL}/${id}`, { nome, unidadeId });
  }

  remover(id: number): Observable<void> {
    return this.http.delete<void>(`${API_URL}/${id}`);
  }
}
