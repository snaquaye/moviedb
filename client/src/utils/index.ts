export const convertStringToArray = (list: string) => {
  return list.split(',').map((item) => item.trim());
}