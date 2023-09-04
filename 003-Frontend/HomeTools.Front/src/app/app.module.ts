import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CabecalhoComponent } from './components/cabecalho/cabecalho.component';
import { RodapeComponent } from './components/rodape/rodape.component';
import { HomeComponent } from './components/home/home.component';
import { BotaoMenuComponent } from './components/menu/botao-menu/botao-menu.component';
import { MenuFinancasComponent } from './components/financas/menu-financas/menu-financas.component';
import { PainelContasComponent } from './components/financas/painel-contas/painel-contas.component';
import { PainelDespesasComponent } from './components/financas/painel-despesas/painel-despesas.component';
import { PaginadorComponent } from './components/paginador/paginador/paginador.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

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
    PaginadorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
