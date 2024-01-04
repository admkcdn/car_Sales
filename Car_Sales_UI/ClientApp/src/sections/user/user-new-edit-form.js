import PropTypes from 'prop-types';
import * as Yup from 'yup';
import { useCallback, useMemo } from 'react';
import { useForm, Controller } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { t } from 'i18next';
// @mui
import LoadingButton from '@mui/lab/LoadingButton';
import Box from '@mui/material/Box';
import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import Switch from '@mui/material/Switch';
import Grid from '@mui/material/Unstable_Grid2';
import Typography from '@mui/material/Typography';
import FormControlLabel from '@mui/material/FormControlLabel';
// utils
import { postCreateUser } from 'src/api/user';
import { fData } from 'src/utils/format-number';
// routes
import { paths } from 'src/routes/paths';
import { useRouter } from 'src/routes/hooks';
// assets
import { countries } from 'src/assets/data';
// components
import Label from 'src/components/label';
import Iconify from 'src/components/iconify';
import { useSnackbar } from 'src/components/snackbar';
import FormProvider, {
  RHFSwitch,
  RHFTextField,
  RHFUploadAvatar,
  RHFAutocomplete,
} from 'src/components/hook-form';

// ----------------------------------------------------------------------

export default function UserNewEditForm({ currentUser }) {
  const router = useRouter();

  const { enqueueSnackbar } = useSnackbar();

  const NewUserSchema = Yup.object().shape({
    name: Yup.string().required(t("nameisrequired")),
    surname: Yup.string().required(t("surnameisrequired")),
    email: Yup.string().required(t("emailisrequired")).email(t("emailmustbeavalidemail")),
    phone: Yup.string().required(t("phonenumberisrequired")),
    address: Yup.string().required(t("addressisrequired")),
    username: Yup.string().required(t("usernameisrequired")),
    password: Yup.string().required(t("passwordisrequired")),
  });

  const defaultValues = useMemo(
    () => ({
      name: currentUser?.name || '',
      email: currentUser?.email || '',
      address: currentUser?.address || '',
      phone: currentUser?.phone || '',
      surname: currentUser?.surname || '',
      username: currentUser?.username || '',
      password: currentUser?.password || '',
    }),
    [currentUser]
  );

  const methods = useForm({
    resolver: yupResolver(NewUserSchema),
    defaultValues,
  });

  const {
    reset,
    watch,
    control,
    setValue,
    handleSubmit,
    formState: { isSubmitting },
  } = methods;

  const values = watch();

  const onSubmit = handleSubmit(async (data) => {
    console.log('datadata ', data);
    try {
      await new Promise((resolve) => setTimeout(resolve, 500));
      postCreateUser(data).then((res) => {
        console.log('useCreateUser ', res);
        if (res.data.success) {
          enqueueSnackbar(t('usersaved'));
          reset();
        } else {
          enqueueSnackbar(t('usercantsaved'), { variant: 'error' });
        }
        // router.push(paths.dashboard.user.list);
        console.info('DATA', data);
      });
    } catch (error) {
      console.error(error);
    }
  });

  return (
    <FormProvider methods={methods} onSubmit={onSubmit}>
      <Grid container spacing={3}>
        <Grid xs={12} md={12}>
          <Card sx={{ p: 3 }}>
            <Box
              rowGap={3}
              columnGap={2}
              display="grid"
              gridTemplateColumns={{
                xs: 'repeat(1, 1fr)',
                sm: 'repeat(2, 1fr)',
              }}
            >
              <RHFTextField name="name" label={t('name')} />
              <RHFTextField name="surname" label={t('surname')} />
              <RHFTextField name="email" label="Email" />
              <RHFTextField name="phone" label={t('phonenumber')} />
              <RHFTextField name="username" label={t('username')} />
              <RHFTextField name="address" label={t('address')} />
              <RHFTextField name="password" label={t('password')} />
            </Box>

            <Stack alignItems="flex-end" sx={{ mt: 3 }}>
              <LoadingButton type="submit" variant="contained" loading={isSubmitting}>
                {t('createuser')}
              </LoadingButton>
            </Stack>
          </Card>
        </Grid>
      </Grid>
    </FormProvider>
  );
}

UserNewEditForm.propTypes = {
  currentUser: PropTypes.object,
};
