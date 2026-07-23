import { Component } from '@angular/core';
import { NgIf } from '@angular/common';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthService } from './core/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, NgIf],
  template: `
    <nav *ngIf="auth.usuarioLogado() as login">
      <img class="nav-logo" src="assets/rodonaves-logo.png" alt="Rodonaves Transportes" />
      <a routerLink="/unidades" routerLinkActive="active">Unidades</a>
      <a routerLink="/colaboradores" routerLinkActive="active">Colaboradores</a>
      <a routerLink="/usuarios" routerLinkActive="active">Usuários</a>
      <span class="espaco"></span>
      <span style="color:#9ca3af; font-size: 13px;">{{ login }}</span>
      <button (click)="sair()">Sair</button>
    </nav>
    <router-outlet></router-outlet>
  `
})
export class AppComponent {
  constructor(public auth: AuthService, private router: Router) {}

  sair(): void {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}
