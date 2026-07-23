import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Colaborador, Unidade, Usuario } from '../../core/models';
import { ColaboradorService } from '../../services/colaborador.service';
import { UnidadeService } from '../../services/unidade.service';
import { UsuarioService } from '../../services/usuario.service';

@Component({
  selector: 'app-colaboradores',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="container">
      <div class="page-header">
        <h1>Colaboradores</h1>
        <img class="page-logo" src="assets/rodonaves-logo.png" alt="Rodonaves Transportes" />
      </div>

      <table>
        <thead>
          <tr><th>Código</th><th>Nome</th><th>Unidade</th><th>Usuário</th><th></th></tr>
        </thead>
        <tbody>
          <tr *ngFor="let c of colaboradores">
            <td>{{ c.id }}</td>
            <td>{{ c.nome }}</td>
            <td>{{ c.unidadeNome }}</td>
            <td>{{ c.usuarioLogin }}</td>
            <td><div class="table-actions">
              <button class="secundario" (click)="editar(c)">Editar</button>
              <button class="perigo" (click)="remover(c)">Remover</button>
            </div></td>
          </tr>
        </tbody>
      </table>

      <h2 style="margin-top:32px;">{{ colaboradorEmEdicao ? 'Editar colaborador' : 'Novo colaborador' }}</h2>
      <form (ngSubmit)="salvar()">
        <div>
          <label>Nome</label>
          <input type="text" [(ngModel)]="nome" name="nome" required />
        </div>
        <div>
          <label>Unidade</label>
          <select [(ngModel)]="unidadeId" name="unidadeId" required>
            <option [ngValue]="null" disabled>Selecione</option>
            <option *ngFor="let un of unidadesAtivas" [ngValue]="un.id">{{ un.nome }} ({{ un.codigoUnidade }})</option>
          </select>
        </div>
        <div *ngIf="!colaboradorEmEdicao">
          <label>Usuário vinculado</label>
          <select [(ngModel)]="usuarioId" name="usuarioId" required>
            <option [ngValue]="null" disabled>Selecione</option>
            <option *ngFor="let u of usuarios" [ngValue]="u.id">{{ u.login }}</option>
          </select>
        </div>
        <p class="erro" *ngIf="erro">{{ erro }}</p>
        <div class="form-actions">
          <button type="submit">Salvar</button>
          <button type="button" class="secundario" *ngIf="colaboradorEmEdicao" (click)="cancelarEdicao()">Cancelar</button>
        </div>
      </form>
    </div>
  `
})
export class ColaboradoresComponent implements OnInit {
  colaboradores: Colaborador[] = [];
  unidades: Unidade[] = [];
  usuarios: Usuario[] = [];

  nome = '';
  unidadeId: number | null = null;
  usuarioId: number | null = null;
  colaboradorEmEdicao: Colaborador | null = null;
  erro = '';

  constructor(
    private colaboradorService: ColaboradorService,
    private unidadeService: UnidadeService,
    private usuarioService: UsuarioService
  ) {}

  get unidadesAtivas(): Unidade[] {
    return this.unidades.filter((u) => u.ativa);
  }

  ngOnInit(): void {
    this.carregar();
    this.unidadeService.listar().subscribe((u) => (this.unidades = u));
    this.usuarioService.listar().subscribe((u) => (this.usuarios = u));
  }

  carregar(): void {
    this.colaboradorService.listar().subscribe((colaboradores) => (this.colaboradores = colaboradores));
  }

  editar(colaborador: Colaborador): void {
    this.colaboradorEmEdicao = colaborador;
    this.nome = colaborador.nome;
    this.unidadeId = colaborador.unidadeId;
    this.erro = '';
  }

  cancelarEdicao(): void {
    this.colaboradorEmEdicao = null;
    this.nome = '';
    this.unidadeId = null;
    this.usuarioId = null;
  }

  salvar(): void {
    this.erro = '';

    if (this.colaboradorEmEdicao) {
      this.colaboradorService
        .atualizar(this.colaboradorEmEdicao.id, this.nome, this.unidadeId!)
        .subscribe({
          next: () => { this.cancelarEdicao(); this.carregar(); },
          error: (err) => (this.erro = err?.error?.mensagem ?? 'Erro ao atualizar colaborador.')
        });
      return;
    }

    this.colaboradorService.criar(this.nome, this.unidadeId!, this.usuarioId!).subscribe({
      next: () => { this.cancelarEdicao(); this.carregar(); },
      error: (err) => (this.erro = err?.error?.mensagem ?? 'Erro ao cadastrar colaborador.')
    });
  }

  remover(colaborador: Colaborador): void {
    if (!confirm(`Remover o colaborador ${colaborador.nome}?`)) return;
    this.colaboradorService.remover(colaborador.id).subscribe(() => this.carregar());
  }
}
