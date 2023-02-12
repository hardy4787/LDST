import { UserSettings } from './user-setting.model';

export interface Profile {
  firstName: string;
  lastName: string;
  titlePhotoPath: string | null;
  settings: UserSettings;
}
