import { Categoria } from "../categoria";

export interface ReadConta {
  Id: number;
  Descricao: string;
  ValorInteiro: number;
  ValorCentavos: number;
  DiaVencimento: number;
  Categoria: Categoria;
}
