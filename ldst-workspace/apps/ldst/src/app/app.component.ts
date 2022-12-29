import { Component } from '@angular/core';
import { exampleProducts } from '@ldst-workspace/playground-search';

@Component({
  selector: 'ldst-workspace-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  products = exampleProducts;
}
