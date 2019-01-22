import { TestBed, async, inject } from '@angular/core/testing';

import { AuthLoginSignupGuard } from './auth-login-signup.guard';

describe('AuthLoginSignupGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthLoginSignupGuard]
    });
  });

  it('should ...', inject([AuthLoginSignupGuard], (guard: AuthLoginSignupGuard) => {
    expect(guard).toBeTruthy();
  }));
});
