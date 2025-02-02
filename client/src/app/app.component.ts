import { Component, inject, OnInit } from '@angular/core';
import { NavComponent } from "./nav/nav.component";
import { AccountService } from './_services/account.service';
import { HomeComponent } from "./home/home.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NavComponent, HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  private accountService = inject(AccountService);

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser(){
    const stringUser = localStorage.getItem('user');
    if (!stringUser) return;
    this.accountService.currentUser.set(JSON.parse(stringUser));
  }
}
