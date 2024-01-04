import PropTypes from 'prop-types';
import { useEffect, useCallback } from 'react';
import { Controller, useForm } from 'react-hook-form';
// @mui
import Box from '@mui/material/Box';
import Link from '@mui/material/Link';
import Stack from '@mui/material/Stack';
import { Avatar, ListItemText } from '@mui/material';
import Rating from '@mui/material/Rating';
import Button from '@mui/material/Button';
import Divider from '@mui/material/Divider';
import MenuItem from '@mui/material/MenuItem';
import Typography from '@mui/material/Typography';
import { formHelperTextClasses } from '@mui/material/FormHelperText';
// routes
import { paths } from 'src/routes/paths';
import { useRouter } from 'src/routes/hooks';
// utils
import { fShortenNumber, fCurrency } from 'src/utils/format-number';
// components
import Label from 'src/components/label';
import Iconify from 'src/components/iconify';
import { ColorPicker } from 'src/components/color-utils';
import FormProvider, { RHFSelect } from 'src/components/hook-form';
//
import IncrementerButton from './common/incrementer-button';

// ----------------------------------------------------------------------

export default function ProductDetailsSummary({
  items,
  product,
  onAddCart,
  onGotoStep,
  disabledActions,
  ...other
}) {
  const router = useRouter();

  const { id, title, isActive, createDate, description, cars, users } = product;

  const { price, brand, color, engineVolume, horsePower, kilometer, model, year } = cars;

  const { name, surname, email, address, phone } = users;

  const available = 1;

  const coverUrl =
    'https://platinv2.b2el.net/B2ELResim/AracResim2El/5421/df5ad856-1b84-4cad-a8c2-7a0040c62200_Buyuk.jpg';

  console.log('productproduct ', product);

  const existProduct = !!items?.length && items.map((item) => item.id).includes(id);

  const isMaxQuantity =
    !!items?.length &&
    items.filter((item) => item.id === id).map((item) => item.quantity)[0] >= available;

  const defaultValues = {
    id,
    // name,
    coverUrl,
    available,
    price,
    quantity: available < 1 ? 0 : 1,
  };

  const methods = useForm({
    defaultValues,
  });

  const { reset, watch, control, setValue, handleSubmit } = methods;

  const values = watch();

  useEffect(() => {
    if (product) {
      reset(defaultValues);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [product]);

  const onSubmit = handleSubmit(async (data) => {
    try {
      if (!existProduct) {
        onAddCart?.({
          ...data,
          subTotal: data.price * data.quantity,
        });
      }
      onGotoStep?.(0);
      router.push(paths.product.checkout);
    } catch (error) {
      console.error(error);
    }
  });

  const handleAddCart = useCallback(() => {
    try {
      onAddCart?.({
        ...values,
        subTotal: values.price * values.quantity,
      });
    } catch (error) {
      console.error(error);
    }
  }, [onAddCart, values]);

  const renderPrice = <Box sx={{ typography: 'h5' }}>₺{fCurrency(price)}</Box>;

  const renderUserDetail = (
    <Box>
      <Stack direction="row" alignItems="center" spacing={2}>
        <Avatar
          alt={name}
          src="https://api-prod-minimal-v510.vercel.app/assets/images/avatar/avatar_2.jpg"
          sx={{ width: 48, height: 48 }}
        />

        <ListItemText
          primary={`${name} ${surname}`}
          secondary={`${phone}  -  ${email}`}
          secondaryTypographyProps={{
            component: 'span',
            typography: 'caption',
            mt: 0.5,
            color: 'text.disabled',
          }}
        />

        <Rating value={3} size="small" readOnly precision={0.5} />
      </Stack>
      <Box sx={{ margin: 2 }}>
        Adres:
        <Box component="span" sx={{ color: 'text.secondary', ml: 0.25 }}>
          {address}
        </Box>
      </Box>
    </Box>
  );

  // brand, color, engineVolume, horsePower, kilometer,model,year
  const renderCarInfo = (
    <Box>
      <Box sx={{ margin: 2 }}>
        Marka:
        <Box component="span" sx={{ color: 'text.secondary', ml: 0.25 }}>
          {brand}
        </Box>
      </Box>
      <Box sx={{ margin: 2 }}>
        Model:
        <Box component="span" sx={{ color: 'text.secondary', ml: 0.25 }}>
          {model}
        </Box>
      </Box>
      <Box sx={{ margin: 2 }}>
        Motor Hacmi:
        <Box component="span" sx={{ color: 'text.secondary', ml: 0.25 }}>
          {engineVolume}
        </Box>
      </Box>
      <Box sx={{ margin: 2 }}>
        Beygir:
        <Box component="span" sx={{ color: 'text.secondary', ml: 0.25 }}>
          {horsePower}
        </Box>
      </Box>
      <Box sx={{ margin: 2 }}>
        Kilometre:
        <Box component="span" sx={{ color: 'text.secondary', ml: 0.25 }}>
          {kilometer}
        </Box>
      </Box>
      <Box sx={{ margin: 2 }}>
        Yıl:
        <Box component="span" sx={{ color: 'text.secondary', ml: 0.25 }}>
          {year}
        </Box>
      </Box>
    </Box>
  );

  const renderShare = (
    <Stack direction="row" spacing={3} justifyContent="center">
      <Link
        variant="subtitle2"
        sx={{
          color: 'text.secondary',
          display: 'inline-flex',
          alignItems: 'center',
        }}
      >
        <Iconify icon="mingcute:add-line" width={16} sx={{ mr: 1 }} />
        Compare
      </Link>

      <Link
        variant="subtitle2"
        sx={{
          color: 'text.secondary',
          display: 'inline-flex',
          alignItems: 'center',
        }}
      >
        <Iconify icon="solar:heart-bold" width={16} sx={{ mr: 1 }} />
        Favorite
      </Link>

      <Link
        variant="subtitle2"
        sx={{
          color: 'text.secondary',
          display: 'inline-flex',
          alignItems: 'center',
        }}
      >
        <Iconify icon="solar:share-bold" width={16} sx={{ mr: 1 }} />
        Share
      </Link>
    </Stack>
  );

  const renderQuantity = (
    <Stack direction="row">
      <Typography variant="subtitle2" sx={{ flexGrow: 1 }}>
        Quantity
      </Typography>

      <Stack spacing={1}>
        <IncrementerButton
          name="quantity"
          quantity={values.quantity}
          disabledDecrease={values.quantity <= 1}
          disabledIncrease={values.quantity >= available}
          onIncrease={() => setValue('quantity', values.quantity + 1)}
          onDecrease={() => setValue('quantity', values.quantity - 1)}
        />

        <Typography variant="caption" component="div" sx={{ textAlign: 'right' }}>
          Available: {available}
        </Typography>
      </Stack>
    </Stack>
  );

  // const renderLabels = (newLabel.enabled || saleLabel.enabled) && (
  //   <Stack direction="row" alignItems="center" spacing={1}>
  //     {newLabel.enabled && <Label color="info">{newLabel.content}</Label>}
  //     {saleLabel.enabled && <Label color="error">{saleLabel.content}</Label>}
  //   </Stack>
  // );

  const renderInventoryType = (
    <Box
      component="span"
      sx={{
        typography: 'overline',
        color: (isActive && 'success.main') || (isActive && 'warning.main') || 'success.main',
      }}
    >
      {isActive ? 'İlanda' : 'İlan Dışı'}
    </Box>
  );

  return (
    <FormProvider methods={methods} onSubmit={onSubmit}>
      <Stack spacing={3} sx={{ pt: 3 }} {...other}>
        <Stack spacing={2} alignItems="flex-start">
          {/* {renderLabels} */}

          {renderInventoryType}

          <Typography variant="h5">{title}</Typography>

          {renderPrice}

          {renderUserDetail}

          {renderCarInfo}
        </Stack>

        <Divider sx={{ borderStyle: 'dashed' }} />

        {/* {renderQuantity} */}

        <Divider sx={{ borderStyle: 'dashed' }} />

        {renderShare}
      </Stack>
    </FormProvider>
  );
}

ProductDetailsSummary.propTypes = {
  items: PropTypes.array,
  disabledActions: PropTypes.bool,
  onAddCart: PropTypes.func,
  onGotoStep: PropTypes.func,
  product: PropTypes.object,
};
