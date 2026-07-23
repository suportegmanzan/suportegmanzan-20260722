import { Routes } from '@angular/router';
import { authGuard } from './core/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'unidades', pathMatch: 'full' },
  {
    path: 'login',
    loadComponent: () => import('./pages/login/login.component').then((m) => m.LoginComponent)
  },
  {
    path: 'unidades',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/unidades/unidades.component').then((m) => m.UnidadesComponent)
  },
  {
    path: 'colaboradores',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./pages/colaboradores/colaboradores.component').then((m) => m.ColaboradoresComponent)
  },
  {
    path: 'usuarios',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/usuarios/usuarios.component').then((m) => m.UsuariosComponent)
  },
  { path: '**', redirectTo: 'unidades' }
];
