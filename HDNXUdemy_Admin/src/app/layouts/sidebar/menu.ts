import { MenuItem } from "./menu.model";

export const MENU: MenuItem[] = [
    {
        id: 1,
        label: 'MENUITEMS.MENU.TEXT',
        isTitle: true
    },
    {
        id: 2,
        label: 'MENUITEMS.DASHBOARD.TEXT',
        icon: 'ph-gauge',
        subItems: [
            {
                id: 7,
                label: 'MENUITEMS.DASHBOARD.LIST.REALESTATE',
                link: '/real-estate',
                parentId: 2
            }
        ]
    },
    {
        id: 8,
        label: 'MENUITEMS.APPS.TEXT',
        isTitle: true
    },
    {
        id: 12,
        label: 'MENUITEMS.APPS.LIST.MANAGERSYSTEMS',
        icon: 'ph-storefront',
        parentId: 8,
        subItems: [
            {
                id: 13,
                label: 'MENUITEMS.APPS.LIST.MANAGERNEWS',
                link: '/manager-system/list-news',
                parentId: 12
            },
            {
                id: 14,
                label: 'MENUITEMS.APPS.LIST.MANAGERFILE',
                link: '/manager-system/list-file-upload',
                parentId: 12
            },
            {
                id: 15,
                label: 'MENUITEMS.APPS.LIST.MANAGERBANNER',
                link: '/manager-system/list-banners',
                parentId: 12
            },
            {
                id: 16,
                label: 'MENUITEMS.APPS.LIST.MANAGERSYSTEMCONFIG',
                link: '/manager-system/list-config-system',
                parentId: 12
            },
            {
                id: 17,
                label: 'MENUITEMS.APPS.LIST.CATEGORY',
                link: '/manager-system/category',
                parentId: 12
            },
        ]
    },
    {
        id: 24,
        label: 'MENUITEMS.APPS.LIST.LEARNING',
        icon: 'ph-graduation-cap',
        parentId: 8,
        subItems: [
            {
                id: 25,
                label: 'MENUITEMS.APPS.LIST.LISTCOURSES',
                parentId: 24,
                isCollapsed: true,
                link: '/learning/list-course',
            },
            {
                id: 31,
                label: 'MENUITEMS.APPS.LIST.LISTSTUDENT',
                parentId: 24,
                isCollapsed: true,
                link: '/learning/list-student',
            },
            {
                id: 34,
                label: 'MENUITEMS.APPS.LIST.LISTTEACHERS',
                parentId: 24,
                isCollapsed: true,
                link: '/learning/list-teacher',
            },
            {
                id: 34,
                label: 'MENUITEMS.APPS.LIST.LISTONLINETEACHERS',
                parentId: 24,
                isCollapsed: true,
                link: '/learning/list-course-learning-online',
            },

        ]
    },
    {
        id: 39,
        label: 'MENUITEMS.APPS.LIST.INVOICES',
        icon: 'ph-file-text',
        parentId: 8,
        subItems: [
            {
                id: 40,
                label: 'MENUITEMS.APPS.LIST.LISTINVOICE',
                link: '/purchase-order/list-purchase-order',
                parentId: 39
            }
        ]
    },
    {
        id: 43,
        label: 'MENUITEMS.APPS.LIST.SUBSCRIPTIONS',
        icon: 'ph-ticket',
        parentId: 8,
        subItems: [
            {
                id: 44,
                label: 'MENUITEMS.APPS.LIST.LISTSOFPROMOTION',
                link: '/subscriptions/list-promotion',
                parentId: 43
            },
            {
                id: 48,
                label: 'MENUITEMS.APPS.LIST.MANAGERCOUPON',
                link: '/subscriptions/list-coupons',
                parentId: 43
            },
        ]
    },
    {
        id: 46,
        label: 'MENUITEMS.APPS.LIST.USERMANAGER',
        icon: 'ph-buildings',
        parentId: 8,
        subItems: [
            {
                id: 47,
                label: 'MENUITEMS.APPS.LIST.LISTINGGRID',
                link: '/user-manager/list-user-manager',
                parentId: 46
            },
        ]
    },
]