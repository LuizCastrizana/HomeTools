import { Categoria } from "../categoria";
import { PagamentoConta } from "./PagamentoConta";

export interface Conta {
  Id: number;
  Descricao: string;
  ValorInteiro: number;
  ValorCentavos: number;
  DiaVencimento: number;
  Categoria: Categoria;
  Pagamentos: PagamentoConta[];
  UltimoPagamento?: Date;
  Variavel: boolean;
  StatusId: number;
}
