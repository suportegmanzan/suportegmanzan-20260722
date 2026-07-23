import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  template: `
    <div class="container" style="max-width: 380px; margin-top: 80px;">
      <img class="login-logo" src="assets/rodonaves-logo.png" alt="Rodonaves Transportes" />
      
      <form (ngSubmit)="entrar()">
        <div>
          <label>Login</label>
          <input type="text" [(ngModel)]="login" name="login" required />
        </div>
        <div>
          <label>Senha</label>
          <input type="password" [(ngModel)]="senha" name="senha" required />
        </div>
        <p class="erro" *ngIf="erro">{{ erro }}</p>
        <button type="submit" [disabled]="carregando">Entrar</button>
      </form>
    </div>
  `
})
export class LoginComponent {
  login = '';
  senha = '';
  erro = '';
  carregando = false;

  constructor(private auth: AuthService, private router: Router) {}

  entrar(): void {
    this.erro = '';
    this.carregando = true;
    this.auth.login(this.login, this.senha).subscribe({
      next: () => {
        this.carregando = false;
        this.router.navigate(['/unidades']);
      },
      error: (err) => {
        this.carregando = false;
        this.erro = err?.error?.mensagem ?? 'Não foi possível entrar. Verifique login e senha.';
      }
    });
  }
}
