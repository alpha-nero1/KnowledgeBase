import 'package:flutter/material.dart';
import 'package:meals/widgets/typography.dart';

/// Prevents you from needing to use Theme.of everywhere.
///
class Typography extends StatelessWidget {
  final String text;
  final AppTextType type;
  final AppSchemeColor? schemeColor;
  final TextStyle? style;
  final TextAlign? textAlign;
  final int? maxLines;
  final TextOverflow? overflow;

  const Typography(
    this.text, {
    super.key,
    this.type = AppTextType.bodyMedium,
    this.schemeColor,
    this.style,
    this.textAlign,
    this.maxLines,
    this.overflow,
  });

  TextStyle? _resolveTextStyle(BuildContext context) {
    final textTheme = Theme.of(context).textTheme;

    return switch (type) {
      AppTextType.displayLarge => textTheme.displayLarge,
      AppTextType.displayMedium => textTheme.displayMedium,
      AppTextType.displaySmall => textTheme.displaySmall,
      AppTextType.headlineLarge => textTheme.headlineLarge,
      AppTextType.headlineMedium => textTheme.headlineMedium,
      AppTextType.headlineSmall => textTheme.headlineSmall,
      AppTextType.titleLarge => textTheme.titleLarge,
      AppTextType.titleMedium => textTheme.titleMedium,
      AppTextType.titleSmall => textTheme.titleSmall,
      AppTextType.bodyLarge => textTheme.bodyLarge,
      AppTextType.bodyMedium => textTheme.bodyMedium,
      AppTextType.bodySmall => textTheme.bodySmall,
      AppTextType.labelLarge => textTheme.labelLarge,
      AppTextType.labelMedium => textTheme.labelMedium,
      AppTextType.labelSmall => textTheme.labelSmall,
    };
  }

  Color? _resolveSchemeColor(BuildContext context) {
    final colorScheme = Theme.of(context).colorScheme;
    final value = schemeColor;

    if (value == null) {
      return null;
    }

    return switch (value) {
      AppSchemeColor.primary => colorScheme.primary,
      AppSchemeColor.onPrimary => colorScheme.onPrimary,
      AppSchemeColor.primaryContainer => colorScheme.primaryContainer,
      AppSchemeColor.onPrimaryContainer => colorScheme.onPrimaryContainer,
      AppSchemeColor.secondary => colorScheme.secondary,
      AppSchemeColor.onSecondary => colorScheme.onSecondary,
      AppSchemeColor.secondaryContainer => colorScheme.secondaryContainer,
      AppSchemeColor.onSecondaryContainer => colorScheme.onSecondaryContainer,
      AppSchemeColor.tertiary => colorScheme.tertiary,
      AppSchemeColor.onTertiary => colorScheme.onTertiary,
      AppSchemeColor.tertiaryContainer => colorScheme.tertiaryContainer,
      AppSchemeColor.onTertiaryContainer => colorScheme.onTertiaryContainer,
      AppSchemeColor.error => colorScheme.error,
      AppSchemeColor.onError => colorScheme.onError,
      AppSchemeColor.errorContainer => colorScheme.errorContainer,
      AppSchemeColor.onErrorContainer => colorScheme.onErrorContainer,
      AppSchemeColor.surface => colorScheme.surface,
      AppSchemeColor.onSurface => colorScheme.onSurface,
      AppSchemeColor.onSurfaceVariant => colorScheme.onSurfaceVariant,
      AppSchemeColor.outline => colorScheme.outline,
      AppSchemeColor.outlineVariant => colorScheme.outlineVariant,
      AppSchemeColor.inverseSurface => colorScheme.inverseSurface,
      AppSchemeColor.onInverseSurface => colorScheme.onInverseSurface,
      AppSchemeColor.inversePrimary => colorScheme.inversePrimary,
      AppSchemeColor.shadow => colorScheme.shadow,
      AppSchemeColor.scrim => colorScheme.scrim,
      AppSchemeColor.surfaceTint => colorScheme.surfaceTint,
    };
  }

  TextStyle? _buildStyle(BuildContext context) {
    var resolvedStyle = _resolveTextStyle(context);
    final resolvedColor = _resolveSchemeColor(context);

    if (resolvedColor != null) {
      resolvedStyle = (resolvedStyle ?? const TextStyle()).copyWith(
        color: resolvedColor,
      );
    }

    return resolvedStyle?.merge(style) ?? style;
  }

  @override
  Widget build(BuildContext context) {
    return Text(
      text,
      style: _buildStyle(context),
      textAlign: textAlign,
      maxLines: maxLines,
      overflow: overflow,
    );
  }
}