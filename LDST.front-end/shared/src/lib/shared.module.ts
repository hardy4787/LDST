import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeFormatPipe } from './pipes/time-format.pipe';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  imports: [CommonModule, ToastrModule.forRoot()],
  declarations: [TimeFormatPipe],
  exports: [TimeFormatPipe],
})
export class SharedModule {}
