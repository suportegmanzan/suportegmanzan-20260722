import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Unidade } from '../../core/models';
import { UnidadeService } from '../../services/unidade.service';

@Component({
  selector: 'app-unidades',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="container">
      <div class="page-header">
        <h1>Unidades</h1>
        <img class="page-logo" src="assets/rodonaves-logo.png" alt="Rodonaves Transportes" />
      </div>

      <table>
        <thead>
          <tr><th>Código</th><th>Nome</th><th>Status</th><th>Colaboradores</th><th></th></tr>
        </thead>
        <tbody>
          <tr *ngFor="let un of unidades">
            <td>{{ un.codigoUnidade }}</td>
            <td>{{ un.nome }}</td>
            <td><span class="badge" [class.ativo]="un.ativa" [class.inativo]="!un.ativa">
              {{ un.ativa ? 'Ativa' : 'Inativa' }}
            </span></td>
            <td>
              <span *ngIf="un.colaboradores.length === 0">-</span>
              <span *ngFor="let c of un.colaboradores">{{ c.nome }}; </span>
            </td>
            <td>
              <button class="secundario" *ngIf="un.ativa" (click)="inativar(un)">Inativar</button>
              <button *ngIf="!un.ativa" (click)="ativar(un)">Ativar</button>
            </td>
          </tr>
        </tbody>
      </table>

      <h2 style="margin-top:32px;">Nova unidade</h2>
      <form (ngSubmit)="salvar()">
        <div>
          <label>Código da unidade</label>
          <input type="text" [(ngModel)]="codigoUnidade" name="codigoUnidade" required />
        </div>
        <div>
          <label>Nome</label>
          <input type="text" [(ngModel)]="nome" name="nome" required />
        </div>
        <p class="erro" *ngIf="erro">{{ erro }}</p>
        <button type="submit">Salvar</button>
      </form>
    </div>
  `
})
export class UnidadesComponent implements OnInit {
  unidades: Unidade[] = [];
  codigoUnidade = '';
  nome = '';
  erro = '';

  constructor(private unidadeService: UnidadeService) {}

  ngOnInit(): void {
    this.carregar();
  }

  carregar(): void {
    this.unidadeService.listar().subscribe((unidades) => (this.unidades = unidades));
  }

  salvar(): void {
    this.erro = '';
    this.unidadeService.criar(this.codigoUnidade, this.nome).subscribe({
      next: () => {
        this.codigoUnidade = '';
        this.nome = '';
        this.carregar();
      },
      error: (err) => (this.erro = err?.error?.mensagem ?? 'Erro ao cadastrar unidade.')
    });
  }

  inativar(unidade: Unidade): void {
    this.unidadeService.atualizar(unidade.id, null, false).subscribe(() => this.carregar());
  }

  ativar(unidade: Unidade): void {
    this.unidadeService.atualizar(unidade.id, null, true).subscribe(() => this.carregar());
  }
}
