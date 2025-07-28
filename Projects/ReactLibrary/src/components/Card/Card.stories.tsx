import type { Meta, StoryObj } from '@storybook/react';
import { action } from '@storybook/addon-actions';
import Card from './Card';

const meta = {
  title: 'Components/Card',
  component: Card,
  parameters: {
    layout: 'centered',
  },
  tags: ['autodocs'],
  argTypes: {
    variant: {
      control: { type: 'select' },
      options: ['default', 'elevated', 'outlined'],
    },
    padding: {
      control: { type: 'select' },
      options: ['none', 'small', 'medium', 'large'],
    },
  },
  args: { onClick: action('clicked') },
} satisfies Meta<typeof Card>;

export default meta;
type Story = StoryObj<typeof meta>;

export const Default: Story = {
  args: {
    children: 'This is a basic card with some content.',
  },
};

export const WithTitleAndSubtitle: Story = {
  args: {
    title: 'Card Title',
    subtitle: 'This is a subtitle',
    children: 'This card has both a title and subtitle along with content.',
  },
};

export const Elevated: Story = {
  args: {
    variant: 'elevated',
    title: 'Elevated Card',
    children: 'This card has a shadow effect.',
  },
};

export const Outlined: Story = {
  args: {
    variant: 'outlined',
    title: 'Outlined Card',
    children: 'This card has a border instead of shadow.',
  },
};

export const Clickable: Story = {
  args: {
    clickable: true,
    title: 'Clickable Card',
    subtitle: 'Click me!',
    children: 'This card responds to clicks and has hover effects.',
  },
};

export const SmallPadding: Story = {
  args: {
    padding: 'small',
    title: 'Small Padding',
    children: 'This card has reduced padding.',
  },
};

export const LargePadding: Story = {
  args: {
    padding: 'large',
    title: 'Large Padding',
    children: 'This card has increased padding for more spacious content.',
  },
};

export const NoPadding: Story = {
  args: {
    padding: 'none',
    children: (
      <div style={{ padding: '16px', background: '#f3f4f6' }}>
        This card has no internal padding. The gray background shows the content area.
      </div>
    ),
  },
};
