import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  /* The child component (register) is receiving data from the parent component (home), so we use Input: parent -> child
  @Input() usersFromHomeComponent: any; */
  /* In Angular 17+, the newest way to declare an input is by signals:
  usersFromHomeComponent = input.required<any>(); */
  /* Output: child to parent. Older way:
  @Output() cancelRegister = new EventEmitter();
  */

  // New way with signals
  cancelRegister = output<boolean>();

  accountService = inject(AccountService);
  toastService = inject(ToastrService);
  model: any = {};

  register() {
    this.accountService.register(this.model).subscribe({
      next: (response: any) => {
        console.log(response);
      },
      error: error => this.toastService.error(error.error),
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
