import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfilePageComponent } from './profile-page.component';
import { RouterModule } from '@angular/router';
import { ImageValidators, SharedModule } from '@ldst/shared';
import { ProfileService } from './services/profile.service';
import { ImageCardComponent } from '@ldst/organisms';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild([
      {
        path: ':userName',
        component: ProfilePageComponent,
      },
    ]),
    ImageCardComponent,
  ],
  declarations: [ProfilePageComponent],
  providers: [ProfileService, ImageValidators],
})
export class ProfileModule {}
