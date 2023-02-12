import { UserSettings } from './user-setting.model';

export interface UpdateProfileParams {
  userName: string;
  firstName: string;
  lastName: string;
  settings: UserSettings;
}
