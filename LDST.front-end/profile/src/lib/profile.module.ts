import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfilePageComponent } from './profile-page.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from '@ldst/shared';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild([
      {
        path: '',
        component: ProfilePageComponent,
      },
    ]),
  ],
  declarations: [ProfilePageComponent],
})
export class ProfileModule {}
