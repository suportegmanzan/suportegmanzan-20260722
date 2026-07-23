export enum StatusUsuario {
  Inativo = 0,
  Ativo = 1
}

export interface Usuario {
  id: number;
  login: string;
  status: StatusUsuario;
}

export interface Unidade {
  id: number;
  codigoUnidade: string;
  nome: string;
  ativa: boolean;
  colaboradores: Colaborador[];
}

export interface Colaborador {
  id: number;
  nome: string;
  unidadeId: number;
  unidadeNome: string;
  usuarioLogin: string;
}

export interface LoginResposta {
  token: string;
  expiraEm: string;
  login: string;
}
