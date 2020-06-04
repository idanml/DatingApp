import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  @Output() cancelReg = new EventEmitter();
  model: any = {};
  constructor(private authService: AuthService) {}

  ngOnInit(): void {}
  register() {
    this.authService.register(this.model).subscribe(
      () => {
        console.log('reg seccess');
      },
      (error) => {
        console.log(error);
      }
    );
  }

  cancel() {
    this.cancelReg.emit(false);
    console.log('cancel');
  }
}
