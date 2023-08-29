import { DOCUMENT } from '@angular/common';
import { Component, HostListener, Inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-cabecalho',
  templateUrl: './cabecalho.component.html',
  styleUrls: ['./cabecalho.component.css']
})
export class CabecalhoComponent implements OnInit {

  constructor(@Inject(DOCUMENT) document: Document) { }

  isClickMenu: boolean = false;

  ngOnInit(): void {
  }

  clickMenu() {
    this.isClickMenu = true;
    let menu = document.getElementById("dropdown-content");
    if (menu != null )
      {
      if (menu.style.display == "none" || menu.style.display == "") {
        menu.style.display = "block";
      } else {
        menu.style.display = "none";
      }
    }
  }

  @HostListener("document:click", ["$event"])
  click() {
    let menu = document.getElementById("dropdown-content");
    if (!this.isClickMenu && menu?.style.display == "block") {
      menu.style.display = "none";
    }
    this.isClickMenu = false;
  }
}
