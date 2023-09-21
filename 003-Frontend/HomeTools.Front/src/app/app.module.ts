import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CabecalhoComponent } from './components/cabecalho/cabecalho.component';
import { RodapeComponent } from './components/rodape/rodape.component';
import { HomeComponent } from './components/home/home.component';
import { BotaoMenuComponent } from './components/menu/botao-menu/botao-menu.component';
import { MenuFinancasComponent } from './components/financas/menu-financas/menu-financas.component';
import { PainelContasComponent } from './components/financas/contas/painel-contas/painel-contas.component';
import { PainelDespesasComponent } from './components/financas/despesas/painel-despesas/painel-despesas.component';
import { PaginadorComponent } from './components/paginador/paginador/paginador.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { IncluirContaComponent } from './components/financas/contas/incluir-conta/incluir-conta.component';
import { EditarContaComponent } from './components/financas/contas/editar-conta/editar-conta.component';
import { ExcluirContaComponent } from './components/financas/contas/excluir-conta/excluir-conta.component';
import { FeedbackAlertaComponent } from './components/feedback-alerta/feedback-alerta.component';
import { IncluirPagamentoContaComponent } from './components/financas/contas/incluir-pagamento-conta/incluir-pagamento-conta.component';
import { IncluirDespesaComponent } from './components/financas/despesas/incluir-despesa/incluir-despesa.component';
import { EditarDespesaComponent } from './components/financas/despesas/editar-despesa/editar-despesa.component';
import { ExcluirDespesaComponent } from './components/financas/despesas/excluir-despesa/excluir-despesa.component';
import { PainelCartoesComponent } from './components/financas/cartoes/painel-cartoes/painel-cartoes.component';
import { IncluirCartaoComponent } from './components/financas/cartoes/incluir-cartao/incluir-cartao.component';
import { EditarCartaoComponent } from './components/financas/cartoes/editar-cartao/editar-cartao.component';
import { ExcluirCartaoComponent } from './components/financas/cartoes/excluir-cartao/excluir-cartao.component';
import { AssinaturasComponent } from './components/financas/cartoes/assinaturas/assinaturas.component';
import { ComprasComponent } from './components/financas/cartoes/compras/compras.component';

import { registerLocaleData } from '@angular/common';
import localePT from '@angular/common/locales/pt';
registerLocaleData(localePT);

@NgModule({
  declarations: [
    AppComponent,
    CabecalhoComponent,
    RodapeComponent,
    HomeComponent,
    BotaoMenuComponent,
    MenuFinancasComponent,
    PainelContasComponent,
    PainelDespesasComponent,
    PaginadorComponent,
    IncluirContaComponent,
    EditarContaComponent,
    ExcluirContaComponent,
    FeedbackAlertaComponent,
    IncluirPagamentoContaComponent,
    IncluirDespesaComponent,
    EditarDespesaComponent,
    ExcluirDespesaComponent,
    PainelCartoesComponent,
    IncluirCartaoComponent,
    EditarCartaoComponent,
    ExcluirCartaoComponent,
    AssinaturasComponent,
    ComprasComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-br' },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
