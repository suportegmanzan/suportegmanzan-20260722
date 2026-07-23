import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Unidade } from '../core/models';

const API_URL = 'http://localhost:5000/api/unidades';

@Injectable({ providedIn: 'root' })
export class UnidadeService {
  constructor(private http: HttpClient) {}

  listar(): Observable<Unidade[]> {
    return this.http.get<Unidade[]>(API_URL);
  }

  criar(codigoUnidade: string, nome: string): Observable<Unidade> {
    return this.http.post<Unidade>(API_URL, { codigoUnidade, nome });
  }

  atualizar(id: number, nome: string | null, ativa: boolean | null): Observable<Unidade> {
    return this.http.put<Unidade>(`${API_URL}/${id}`, { nome, ativa });
  }
}
