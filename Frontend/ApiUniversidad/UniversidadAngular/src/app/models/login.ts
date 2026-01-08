export interface LoginDto {
  nombreUsuario: string;
  email: string;
  token: string;
}

export interface LoginUsuarioQuery {
  nombreUsuario: string;
  email: string;
}
