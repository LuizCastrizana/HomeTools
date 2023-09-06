import { Categoria } from "../../categoria";
import { PagamentoContaVariavelDto } from "./readPagamentoContaVariavelDto";

export interface ReadContaVariavelDto {
  Id: number;
  Descricao: string;
  DiaVencimento: number;
  Categoria: Categoria;
  Pagamentos: PagamentoContaVariavelDto[];
}
