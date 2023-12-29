import useSWR from 'swr';
import { useMemo } from 'react';
// utils
import axiosInstance, { fetcher, endpoints } from 'src/utils/axios';

// ----------------------------------------------------------------------

export async function postCreateUser(user) {
    console.log("ben geldimmm");
  const URL = endpoints.user.add;
    console.log("URLURL ",URL);
  const res = await axiosInstance.post(URL, user);
  return res;
}
