import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { StatusUsuario, Usuario } from '../../core/models';
import { UsuarioService } from '../../services/usuario.service';

@Component({
  selector: 'app-usuarios',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="container">
      <div class="page-header">
        <h1>Usuários</h1>
        <img class="page-logo" src="assets/rodonaves-logo.png" alt="Rodonaves Transportes" />
      </div>

      <div style="display:flex; gap:10px; align-items:center;">
        <label style="margin:0;">Filtrar por status:</label>
        <select [(ngModel)]="filtroStatus" (ngModelChange)="carregar()">
          <option [ngValue]="null">Todos</option>
          <option [ngValue]="1">Ativo</option>
          <option [ngValue]="0">Inativo</option>
        </select>
      </div>

      <table>
        <thead>
          <tr><th>Login</th><th>Status</th><th></th></tr>
        </thead>
        <tbody>
          <tr *ngFor="let u of usuarios">
            <td>{{ u.login }}</td>
            <td><span class="badge" [class.ativo]="u.status === 1" [class.inativo]="u.status !== 1">
              {{ u.status === 1 ? 'Ativo' : 'Inativo' }}
            </span></td>
            <td>
              <button class="secundario" (click)="editar(u)">Editar</button>
            </td>
          </tr>
        </tbody>
      </table>

      <h2 style="margin-top:32px;">{{ usuarioEmEdicao ? 'Editar usuário (senha/status)' : 'Novo usuário' }}</h2>
      <form (ngSubmit)="salvar()">
        <div *ngIf="!usuarioEmEdicao">
          <label>Login</label>
          <input type="text" [(ngModel)]="novoLogin" name="novoLogin" required />
        </div>
        <div *ngIf="usuarioEmEdicao">
          <label>Login</label>
          <input type="text" [value]="usuarioEmEdicao.login" disabled />
        </div>
        <div>
          <label>Senha {{ usuarioEmEdicao ? '(deixe em branco para manter)' : '' }}</label>
          <input type="password" [(ngModel)]="senha" name="senha" [required]="!usuarioEmEdicao" />
        </div>
        <div>
          <label>Status</label>
          <select [(ngModel)]="status" name="status">
            <option [ngValue]="1">Ativo</option>
            <option [ngValue]="0">Inativo</option>
          </select>
        </div>
        <p class="erro" *ngIf="erro">{{ erro }}</p>
        <div class="form-actions">
          <button type="submit">Salvar</button>
          <button type="button" class="secundario" *ngIf="usuarioEmEdicao" (click)="cancelarEdicao()">Cancelar</button>
        </div>
      </form>
    </div>
  `
})
export class UsuariosComponent implements OnInit {
  usuarios: Usuario[] = [];
  filtroStatus: number | null = null;

  novoLogin = '';
  senha = '';
  status: StatusUsuario = StatusUsuario.Ativo;
  usuarioEmEdicao: Usuario | null = null;
  erro = '';

  constructor(private usuarioService: UsuarioService) {}

  ngOnInit(): void {
    this.carregar();
  }

  carregar(): void {
    const status = this.filtroStatus === null ? undefined : (this.filtroStatus as StatusUsuario);
    this.usuarioService.listar(status).subscribe((usuarios) => (this.usuarios = usuarios));
  }

  editar(usuario: Usuario): void {
    this.usuarioEmEdicao = usuario;
    this.status = usuario.status;
    this.senha = '';
    this.erro = '';
  }

  cancelarEdicao(): void {
    this.usuarioEmEdicao = null;
    this.novoLogin = '';
    this.senha = '';
    this.status = StatusUsuario.Ativo;
  }

  salvar(): void {
    this.erro = '';

    if (this.usuarioEmEdicao) {
      this.usuarioService
        .atualizar(this.usuarioEmEdicao.id, this.senha || null, this.status)
        .subscribe({
          next: () => { this.cancelarEdicao(); this.carregar(); },
          error: (err) => (this.erro = err?.error?.mensagem ?? 'Erro ao atualizar usuário.')
        });
      return;
    }

    this.usuarioService.criar(this.novoLogin, this.senha, this.status).subscribe({
      next: () => { this.cancelarEdicao(); this.carregar(); },
      error: (err) => (this.erro = err?.error?.mensagem ?? 'Erro ao cadastrar usuário.')
    });
  }
}
