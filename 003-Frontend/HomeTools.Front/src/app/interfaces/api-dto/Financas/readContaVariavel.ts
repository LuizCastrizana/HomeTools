import { Categoria } from "../../categoria";
import { PagamentoContaVariavel } from "./readPagamentoContaVariavel";

export interface ReadContaVariavel {
  Id: number;
  Descricao: string;
  DiaVencimento: number;
  Categoria: Categoria;
  Pagamentos: PagamentoContaVariavel[];
}
