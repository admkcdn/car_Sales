import PropTypes from 'prop-types';
import * as Yup from 'yup';
import { useCallback, useMemo, useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
// @mui
import LoadingButton from '@mui/lab/LoadingButton';
import Box from '@mui/material/Box';
import Chip from '@mui/material/Chip';
import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
import Switch from '@mui/material/Switch';
import Divider from '@mui/material/Divider';
import Grid from '@mui/material/Unstable_Grid2';
import CardHeader from '@mui/material/CardHeader';
import Typography from '@mui/material/Typography';
import InputAdornment from '@mui/material/InputAdornment';
import FormControlLabel from '@mui/material/FormControlLabel';
// routes
import { t } from 'i18next';
import { paths } from 'src/routes/paths';
// hooks
import { useResponsive } from 'src/hooks/use-responsive';
// _mock
import {
  _tags,
  PRODUCT_SIZE_OPTIONS,
  PRODUCT_GENDER_OPTIONS,
  PRODUCT_COLOR_NAME_OPTIONS,
  PRODUCT_CATEGORY_GROUP_OPTIONS,
} from 'src/_mock';
// components
import { useSnackbar } from 'src/components/snackbar';
import { useRouter } from 'src/routes/hooks';
import FormProvider, {
  RHFSelect,
  RHFEditor,
  RHFUpload,
  RHFSwitch,
  RHFTextField,
  RHFMultiSelect,
  RHFAutocomplete,
  RHFMultiCheckbox,
} from 'src/components/hook-form';
import { postAdvert } from 'src/api/advert';

// ----------------------------------------------------------------------

export default function ProductNewEditForm({ currentProduct }) {
  const router = useRouter();

  const mdUp = useResponsive('up', 'md');

  const { enqueueSnackbar } = useSnackbar();

  const [includeTaxes, setIncludeTaxes] = useState(false);

  const NewProductSchema = Yup.object().shape({
    name: Yup.string().required('Name is required'),
    // images: Yup.array().min(1, 'Images is required'),
    subDescription: Yup.string().required('SubDescription is required'),
    brand: Yup.string().required('brand is required'),
    model: Yup.string().required('model is required'),
    kilometer: Yup.string().required('kilometer is required'),
    horsepower: Yup.string().required('horsepower is required'),
    quantity: Yup.string().required('quantity is required'),
    color: Yup.string().required('color is required'),
    // fuelType: Yup.object().shape({
    //   value: Yup.string(),
    //   label: Yup.string(),
    // }),
    // not required
  });

  const defaultValues = useMemo(
    () => ({
      name: currentProduct?.name || '',
      subDescription: currentProduct?.subDescription || '',
      brand: currentProduct?.brand || '',
      model: currentProduct?.model || '',
      kilometer: currentProduct?.kilometer || '',
      horsepower: currentProduct?.horsepower || '',
      color: currentProduct?.color || '',
      // images: currentProduct?.images || [],
      // transmissionType: null,
      // fuelType: null,
      //
      quantity: currentProduct?.quantity || '',
    }),
    [currentProduct]
  );

  const methods = useForm({
    resolver: yupResolver(NewProductSchema),
    defaultValues,
  });

  const {
    reset,
    watch,
    setValue,
    handleSubmit,
    formState: { isSubmitting },
  } = methods;

  const values = watch();

  useEffect(() => {
    if (currentProduct) {
      reset(defaultValues);
    }
  }, [currentProduct, defaultValues, reset]);

  const onSubmit = handleSubmit(async (data) => {
    try {
      console.log('datadatadata ', data);
      await new Promise((resolve) => setTimeout(resolve, 500));
      postAdvert(data).then((res) => {
        if (res.data.success) {
          enqueueSnackbar(t('productcreated'));
          reset();
        } else {
          enqueueSnackbar(t('productcantcreated'), { variant: 'error' });
        }
      });
      // router.push(paths.dashboard.product.root);
      console.info('DATA', data);
    } catch (error) {
      console.error(error);
    }
  });

  const handleDrop = useCallback(
    (acceptedFiles) => {
      const files = values.images || [];

      const newFiles = acceptedFiles.map((file) =>
        Object.assign(file, {
          preview: URL.createObjectURL(file),
        })
      );

      setValue('images', [...files, ...newFiles], { shouldValidate: true });
    },
    [setValue, values.images]
  );

  const handleRemoveFile = useCallback(
    (inputFile) => {
      const filtered = values.images && values.images?.filter((file) => file !== inputFile);
      setValue('images', filtered);
    },
    [setValue, values.images]
  );

  const handleRemoveAllFiles = useCallback(() => {
    setValue('images', []);
  }, [setValue]);

  const handleChangeIncludeTaxes = useCallback((event) => {
    setIncludeTaxes(event.target.checked);
  }, []);

  const renderDetails = (
    <>
      {mdUp && (
        <Grid md={4}>
          <Typography variant="h6" sx={{ mb: 0.5 }}>
            İlan Detayları
          </Typography>
          <Typography variant="body2" sx={{ color: 'text.secondary' }}>
            İlanınız hakkında başlık ve açıklama giriniz...
          </Typography>
        </Grid>
      )}

      <Grid xs={12} md={8}>
        <Card>
          {!mdUp && <CardHeader title="Details" />}

          <Stack spacing={3} sx={{ p: 3 }}>
            <RHFTextField name="name" label="İlan Adı" />

            <RHFTextField name="subDescription" label="İlan Açıklaması" multiline rows={4} />

            <Stack spacing={1.5}>
              <Typography variant="subtitle2">Resimler</Typography>
              <RHFUpload
                multiple
                thumbnail
                name="images"
                onDrop={handleDrop}
                onRemove={handleRemoveFile}
                onRemoveAll={handleRemoveAllFiles}
                onUpload={() => console.info('ON UPLOAD')}
              />
            </Stack>
          </Stack>
        </Card>
      </Grid>
    </>
  );

  const renderProperties = (
    <>
      {mdUp && (
        <Grid md={4}>
          <Typography variant="h6" sx={{ mb: 0.5 }}>
            Araç Bilgileri
          </Typography>
          <Typography variant="body2" sx={{ color: 'text.secondary' }}>
            Araç bilgileri hakkında detaylı bilgleri giriniz...
          </Typography>
        </Grid>
      )}

      <Grid xs={12} md={8}>
        <Card>
          {!mdUp && <CardHeader title="Properties" />}

          <Stack spacing={3} sx={{ p: 3 }}>
            <Box
              columnGap={2}
              rowGap={3}
              display="grid"
              gridTemplateColumns={{
                xs: 'repeat(1, 1fr)',
                md: 'repeat(2, 1fr)',
              }}
            >
              <RHFTextField name="brand" label="Marka" />

              <RHFTextField name="model" label="Model" />

              <RHFTextField name="kilometer" label="Kilometre" />

              <RHFTextField name="horsepower" label="Beygir" />

              <RHFTextField
                name="quantity"
                label="Adet"
                placeholder="0"
                InputLabelProps={{ shrink: true }}
              />

              <RHFAutocomplete
                label="Yakıt Tipi"
                name="fuelType"
                options={[{ label: 'Benzin', value: '1' },{ label: 'Dizel', value: '2' },{ label: 'Tüp + Benzin', value: '3' }]}
              />

              <RHFTextField name="color" label="Renk" />

              <RHFAutocomplete
                name="transmissionType"
                label="Şanzıman"
                options={[
                  { label: 'Otomatik', value: '1' },
                  { label: 'Manuel', value: '2' },
                ]}
              />
            </Box>

            <Divider sx={{ borderStyle: 'dotted' }} />
          </Stack>
        </Card>
      </Grid>
    </>
  );

  const renderPricing = (
    <>
      {mdUp && (
        <Grid md={4}>
          <Typography variant="h6" sx={{ mb: 0.5 }}>
            Fiyat
          </Typography>
          <Typography variant="body2" sx={{ color: 'text.secondary' }}>
            Aracınızıa biçilen satış fiyatı
          </Typography>
        </Grid>
      )}

      <Grid xs={12} md={8}>
        <Card>
          {!mdUp && <CardHeader title="Pricing" />}

          <Stack spacing={3} sx={{ p: 3 }}>
            <RHFTextField
              name="price"
              label="Fiyat"
              placeholder="0.00"
              InputLabelProps={{ shrink: true }}
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <Box component="span" sx={{ color: 'text.disabled' }}>
                      ₺
                    </Box>
                  </InputAdornment>
                ),
              }}
            />

            <FormControlLabel
              control={<Switch checked={includeTaxes} onChange={handleChangeIncludeTaxes} />}
              label="Pazarlık payına açık bir fiyatlandırma mı ?"
            />
          </Stack>
        </Card>
      </Grid>
    </>
  );

  const renderActions = (
    <>
      {mdUp && <Grid md={4} />}
      <Grid xs={12} md={8} sx={{ display: 'flex', alignItems: 'center' }}>
        <LoadingButton type="submit" variant="contained" size="large" loading={isSubmitting}>
          İlan Oluştur
        </LoadingButton>
      </Grid>
    </>
  );

  return (
    <FormProvider methods={methods} onSubmit={onSubmit}>
      <Grid container spacing={3}>
        {renderDetails}

        {renderProperties}

        {renderPricing}

        {renderActions}
      </Grid>
    </FormProvider>
  );
}

ProductNewEditForm.propTypes = {
  currentProduct: PropTypes.object,
};
