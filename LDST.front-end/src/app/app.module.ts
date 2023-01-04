import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { SharedModule } from '@ldst/shared';
import { RouterModule } from '@angular/router';
import { ToolbarModule } from '@ldst/organisms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    SharedModule,
    RouterModule.forRoot([
      {
        path: 'playground-overview',
        loadChildren: () =>
          import('@ldst/playground-overview').then(
            (m) => m.PlaygroundOverviewModule
          ),
      },
      {
        path: 'playground-selector',
        loadChildren: () =>
          import('@ldst/playground-selector').then(
            (m) => m.PlaygroundSelectorModule
          ),
      },
      {
        path: 'create-playground',
        loadChildren: () =>
          import('@ldst/create-playground').then(
            (m) => m.CreatePlaygroundModule
          ),
      },
      {
        path: 'playground-search',
        loadChildren: () =>
          import('@ldst/playground-search').then(
            (m) => m.PlaygroundSearchModule
          ),
      },
      {
        path: '**',
        loadChildren: () =>
          import('@ldst/playground-search').then(
            (m) => m.PlaygroundSearchModule
          ),
      },
    ]),
    HttpClientModule,
    BrowserAnimationsModule,
    ToolbarModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
