import useSWR from 'swr';
import { useMemo } from 'react';
// utils
import axiosInstance, { fetcher, endpoints } from 'src/utils/axios';

// ----------------------------------------------------------------------

export async function postAdvert(advert){
   
    delete advert.images;

    console.log("ben geldimmm",advert);
    const URL = endpoints.advert.add;
      console.log("URLURL ",URL);
    const res = await axiosInstance.post(URL, advert);
    return res;
}