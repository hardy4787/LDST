import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '@ldst/shared';
import { AboutDialogComponent } from './components/about-dialog/about-dialog.component';
import { ContactsDialogComponent } from './components/contacts-dialog/contacts-dialog.component';
import { ToolbarComponent } from './toolbar.component';

@NgModule({
  declarations: [
    ToolbarComponent,
    AboutDialogComponent,
    ContactsDialogComponent,
  ],
  exports: [ToolbarComponent],
  imports: [CommonModule, SharedModule],
})
export class ToolbarModule {}
