import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrivacyPageComponent } from './privacy-page.component';
import { RouterModule } from '@angular/router';
import { PrivacyService } from './services/privacy.service';
import { SharedModule } from '@ldst/shared';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild([
      {
        path: '',
        component: PrivacyPageComponent,
      },
    ]),
  ],
  providers: [PrivacyService],
  declarations: [PrivacyPageComponent],
})
export class PrivacyModule {}
