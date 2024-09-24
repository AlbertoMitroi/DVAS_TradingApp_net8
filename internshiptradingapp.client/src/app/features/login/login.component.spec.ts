import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { of, throwError } from 'rxjs';
import { AuthService } from '../../_services/auth.service';
import { LoginComponent } from './login.component';
import { User } from '../../_models/user';
import { PasswordModule } from 'primeng/password';

const mockUser: User = {
    username: 'testUser',
    email: 'test@example.com',
    token: 'mockToken123'
  };

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authService: jasmine.SpyObj<AuthService>;
  let router: jasmine.SpyObj<Router>;
  let toastr: jasmine.SpyObj<ToastrService>;

  beforeEach(async () => {
    const authServiceSpy = jasmine.createSpyObj('AuthService', ['login']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigate']);
    const toastrSpy = jasmine.createSpyObj('ToastrService', ['error']);

    await TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        PasswordModule,
    ],
      declarations: [LoginComponent],
      providers: [
        { provide: AuthService, useValue: authServiceSpy },
        { provide: Router, useValue: routerSpy },
        { provide: ToastrService, useValue: toastrSpy }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    authService = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
    router = TestBed.inject(Router) as jasmine.SpyObj<Router>;
    toastr = TestBed.inject(ToastrService) as jasmine.SpyObj<ToastrService>;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have an invalid form when fields are empty', () => {
    expect(component.loginForm.valid).toBeFalsy();
  });

  it('should have a valid form when fields are filled correctly', () => {
    component.loginForm.setValue({
      email: 'test@example.com',
      password: 'password123',
      rememberMe: false
    });

    expect(component.loginForm.valid).toBeTruthy();
  });

  it('should call AuthService login on form submit when form is valid', () => {
    component.loginForm.setValue({
      email: 'test@example.com',
      password: 'password123',
      rememberMe: false
    });

    authService.login.and.returnValue(of(mockUser)); 
    component.onSubmit();

    expect(authService.login).toHaveBeenCalledWith({
      email: 'test@example.com',
      password: 'password123',
      rememberMe: false
    });
  });

  it('should navigate to home on successful login', () => {
    component.loginForm.setValue({
      email: 'test@example.com',
      password: 'password123',
      rememberMe: false
    });

    authService.login.and.returnValue(of(mockUser));
    component.onSubmit();

    expect(router.navigate).toHaveBeenCalledWith(['/']);
  });

  it('should display an error message on failed login', () => {
    component.loginForm.setValue({
      email: 'test@example.com',
      password: 'password123',
      rememberMe: false
    });

    authService.login.and.returnValue(throwError({ error: 'Invalid credentials' }));
    component.onSubmit();

    expect(toastr.error).toHaveBeenCalledWith('Invalid credentials');
  });

  it('should not submit if form is invalid', () => {
    component.loginForm.setValue({
      email: '',
      password: '',
      rememberMe: false
    });

    component.onSubmit();

    expect(authService.login).not.toHaveBeenCalled();
  });
});
