import { Component, OnInit } from '@angular/core';
import { AuthenticationStatusService } from '@ldst/shared';

@Component({
  selector: 'ldst-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'ldst';

  constructor(private authStatusService: AuthenticationStatusService) {}

  ngOnInit(): void {
    if (this.authStatusService.isUserAuthenticated())
      this.authStatusService.sendAuthStateChangeNotification(true);
  }
}
