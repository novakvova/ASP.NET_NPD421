import {createApi} from "@reduxjs/toolkit/query/react";
import type {ICategoryItem} from "../types/category/ICategoryItem.ts";
import {serverBaseQuery} from "../utils/fetchBaseQuery.ts";
import type {ICategoryCreate} from "../types/category/ICategoryCreate.ts";
import {serialize} from "object-to-formdata";


export const apiCategory = createApi({
    reducerPath: 'apiCategory',
    baseQuery: serverBaseQuery("categories"),
    tagTypes: ['Categories'],
    endpoints: (builder) => ({
        getCategories: builder.query<ICategoryItem[],void>({
            query: () => ({
                url: "",
                method: "GET"
            }),
            providesTags: ['Categories'], //Цей кеш називається Categories
        }),

        categoryCreate: builder.mutation<void, ICategoryCreate>({
            query: (model) => {
                try {
                    const formData = serialize(model);
                    return {
                        method: "POST",
                        url: "",
                        body: formData
                    }
                }
                catch {
                    throw new Error("Помилка перетворення даних")
                }
            },
            invalidatesTags: ["Categories"]
        }),
        deleteCategory: builder.mutation<void, number>({
            query: (id) => {
                return {
                    url: `${id}`,
                    method: "DELETE"
                }
            },
            invalidatesTags: ['Categories'],
        }),
    }),
});

export const {
    useGetCategoriesQuery,
    useCategoryCreateMutation,
    useDeleteCategoryMutation
} = apiCategory;